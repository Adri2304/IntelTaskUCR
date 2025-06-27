import { Component, inject, Inject, OnInit } from '@angular/core';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../../services/TaskService/task-service';
import { IUpdateTask } from '../../models/iupdate-task';


@Component({
  selector: 'app-task-form',
  imports: [MatDialogModule, MatButtonModule, MatFormFieldModule, MatSelectModule, MatInputModule,
    MatDatepickerModule, FormsModule],
  providers: [provideNativeDateAdapter()],
  templateUrl: './task-form.html',
  styleUrl: './task-form.css'
})

export class TaskForm implements OnInit {
  title: string = "";
  description: string = "";
  prioritySelected = "1";
  complexitySelected = "4";
  limitDate!: Date;
  selectedTime: string = '16:30';
  gisNumber: string = "";
  dialogRef = inject(MatDialogRef);
  taskService = inject(TaskService);

  constructor(@Inject(MAT_DIALOG_DATA) public data?: IUpdateTask){}

  ngOnInit(): void {
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
}
