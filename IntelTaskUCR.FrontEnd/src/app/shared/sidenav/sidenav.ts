import {ChangeDetectionStrategy, Component, inject, OnInit} from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import {FormBuilder, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { Stateservice } from '../../services/stateService/stateservice';
import { IStates } from '../../models/istates';
import { ConfirmationDialog } from '../confirmation-dialog/confirmation-dialog';
import { MatDialog } from '@angular/material/dialog';


@Component({
  selector: 'app-sidenav',
  imports: [MatSidenavModule, RouterOutlet, FormsModule, ReactiveFormsModule, MatCheckboxModule],
  templateUrl: './sidenav.html',
  styleUrl: './sidenav.css'
})
export class Sidenav implements OnInit {
  private readonly _formBuilder = inject(FormBuilder);
  private readonly stateService = inject(Stateservice);
  private readonly dialog = inject(MatDialog);

  estadosTareas: IStates[] = [];
  selectedStateIds: number[] = [];

  // Grupo reactivo para los checkboxes
  estados = this._formBuilder.group({});

  ngOnInit(): void {
    this.stateService.getAllStates().subscribe({
      next: (res: IStates[]) => {

        this.estadosTareas = res;

        const group: any = {};
        for (const estado of res) {
          group[estado.cnIdEstado] = false;
        }
        this.estados = this._formBuilder.group(group);

        // Escuchar los cambios en el formulario
        this.estados.valueChanges.subscribe((value) => {
          this.selectedStateIds = [];

          for (const [key, isChecked] of Object.entries(value)) {
            if (isChecked) {
              this.selectedStateIds.push(Number(key));
            }
          }
          this.stateService.updateSelectedIds(this.selectedStateIds);
        });
      },
      error: () => {
        this.dialog.open(ConfirmationDialog, {
          data: {
            title: 'Error',
            body: 'Error al cargar los estados',
            successButton: 'aceptar',
            rejectButton: ''
          }
        });
      }
    });
  }
}

