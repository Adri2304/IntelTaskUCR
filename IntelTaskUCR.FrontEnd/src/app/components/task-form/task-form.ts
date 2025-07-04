import { Component, inject, Inject, OnInit } from '@angular/core';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../../services/TaskService/task-service';
import { IUpdateTask } from '../../models/iupdate-task';
import { CommonModule } from '@angular/common';
import { Stateservice } from '../../services/stateService/stateservice';
import { IComplexities } from '../../models/icomplexities';
import { ConfirmationDialog } from '../../shared/confirmation-dialog/confirmation-dialog';
import { IPriorities } from '../../models/ipriorities';


@Component({
  selector: 'app-task-form',
  imports: [MatDialogModule, MatButtonModule, MatFormFieldModule, MatSelectModule, MatInputModule,
    MatDatepickerModule, FormsModule, CommonModule],
  providers: [provideNativeDateAdapter()],
  templateUrl: './task-form.html',
  styleUrl: './task-form.css'
})

export class TaskForm implements OnInit {
  dialogRef = inject(MatDialogRef);
  taskService = inject(TaskService);
  stateService = inject(Stateservice);
  dialog = inject(MatDialog);

  title: string = "";
  description: string = "";
  prioritySelected = "";
  complexitySelected = "";
  limitDate!: Date;
  selectedTime: string = '16:30';
  gisNumber: string = "";
  minDate: Date;

  complejidades: IComplexities[] = [];
  prioridades: IPriorities[] = [];

  constructor(@Inject(MAT_DIALOG_DATA) public data?: IUpdateTask){
    this.minDate = new Date();
  }

  async ngOnInit(): Promise<void> {
    await this.cargarComplejidades();
    await this.cargarPrioridades();

    if (this.data) {
      const fecha = new Date(this.data.cfFechaLimite);
      this.title = this.data.ctTituloTarea;
      this.description = this.data.ctDescripcionTarea;
      this.prioritySelected = String(this.data.cnIdPrioridad);
      this.complexitySelected = String(this.data.cnIdComplejidad);
      this.limitDate = fecha;
      this.selectedTime = `${fecha.getHours().toString().padStart(2, '0')}:${fecha.getMinutes().toString().padStart(2, '0')}`;
      this.gisNumber = this.data.cnNumeroGis;
    }
  }

  returnData() {
    const fechaLimite = this.formatearFecha(this.combinarFechaHora(this.limitDate, this.selectedTime));

    const nuevaTarea = {
      ctTituloTarea: this.title,
      ctDescripcionTarea: this.description,
      cnIdComplejidad: parseInt(this.complexitySelected),
      cnIdPrioridad: parseInt(this.prioritySelected),
      cnNumeroGis: this.gisNumber,
      cfFechaLimite: fechaLimite,
    };

    this.dialogRef.close(nuevaTarea);
  }

  formatearFecha(fecha: Date): string {
    return fecha.toISOString().split('.')[0]; // Elimina los milisegundos
  }

  combinarFechaHora(fecha: Date, hora: string): Date {
    if (!fecha || !hora) return new Date();

    const [horas, minutos] = hora.split(':').map(Number);
    const nuevaFecha = new Date(fecha);
    nuevaFecha.setHours(horas, minutos, 0, 0);
    return nuevaFecha;
  }

  async cargarComplejidades(){
    this.stateService.getAllComplexities().subscribe({
      next: (res: IComplexities[]) => this.complejidades = res,
      error: () => {
        this.dialog.open(ConfirmationDialog, {
            data: {
              title: "Error",
              body: "Error al cargar las complejidades",
              successButton: "aceptar",
              rejectButton: ""
            }
          });
      }
    });
  }
  
  async cargarPrioridades(){
    this.stateService.getAllPriorities().subscribe({
      next: (res: IPriorities[]) => this.prioridades = res,
      error: () => {
        this.dialog.open(ConfirmationDialog, {
            data: {
              title: "Error",
              body: "Error al cargar los estados",
              successButton: "aceptar",
              rejectButton: ""
            }
          });
      }
    });
  }
}
