import { Component, OnInit, inject } from '@angular/core';
import { MatButtonModule} from '@angular/material/button'
import { ITask } from '../../models/itask';
import { TaskService } from '../../services/TaskService/task-service';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { CommonModule } from '@angular/common';
import { TaskForm } from '../task-form/task-form';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialog } from '../../shared/confirmation-dialog/confirmation-dialog';
import { IChangeStateTask } from '../../models/ichange-state-task';
import { Stateservice } from '../../services/stateService/stateservice';
import { forkJoin, firstValueFrom  } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { AsignarForm } from '../asignar-form/asignar-form';

@Component({
  selector: 'app-task',
  imports: [MatButtonModule, MatCardModule, MatIconModule, MatFormFieldModule, MatSelectModule, MatCheckboxModule,
  CommonModule, FormsModule],
  templateUrl: './task.html',
  styleUrl: './task.css'
})
export class Task implements OnInit{
  tasks: ITask[] = [];
  selectedTaskId: number = 0;
  taskService: TaskService = inject(TaskService)
  dialog = inject(MatDialog);
  stateService = inject(Stateservice);
  idUsuarioLogueado = 7;

  nombresEstados: { [id: number]: string } = {
    1: 'Registrada',
    2: 'Asignada',
    3: 'En proceso',
    4: 'En espera',
    5: 'En revisión',
    6: 'Rechazada',
    7: 'Terminada',
    8: 'Incumplimiento'
  };

  ngOnInit(): void {
    this.cargar_tareas();
  }

  onCheckboxChange(taskId: number) {
  this.selectedTaskId = this.selectedTaskId === taskId ? 0 : taskId;
  }

  cargar_tareas() {
    this.taskService.ReadTaskPerUser(this.idUsuarioLogueado).subscribe({
      next: (data) => {
        if (data === null) {
          return;
        }
        // Paso 1: convertir fechas de las tareas
        const tareas: ITask[] = data.map((task: ITask) => ({
          ...task,
          cfFechaAsignacion: task.cfFechaAsignacion ? new Date(task.cfFechaAsignacion) : null,
          cfFechaLimite: new Date(task.cfFechaLimite),
          cfFechaFinalizacion: task.cfFechaFinalizacion ? new Date(task.cfFechaFinalizacion) : null,
        }));
        
        // Paso 2: crear observables para estados posteriores
        const estadosObservables = tareas.map((task) =>
          this.stateService.getEstadosPosteriores(task.cnIdEstado)
        );

        // Paso 3: esperar todas las respuestas
        forkJoin(estadosObservables).subscribe({
          next: (estadosPorTarea: number[][]) => {
            this.tasks = tareas.map((task, i) => ({
              ...task,
              estadosDisponibles: estadosPorTarea[i], // Este campo es nuevo, opcional en ITask
            }));
          },
          error: (error) => {
            console.error('Error consultando estados posteriores:', error);
          }
        });
        
      },
      error: (err) => {
        console.error(err);
        alert("Error al cargar tareas");
      }
    });
  }

  createTask(){
    const dialogRef = this.dialog.open(TaskForm);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      if (result) {
        const nuevaTarea = {
          cnTareaOrigen: null,
          ctTituloTarea: result.ctTituloTarea,
          ctDescripcionTarea: result.ctDescripcionTarea,
          ctDescripcionEspera: null,
          cnIdComplejidad: result.cnIdComplejidad,
          cnIdEstado: 1,
          cnIdPrioridad: result.cnIdPrioridad,
          cnNumeroGis: result.cnNumeroGis,
          cfFechaAsignacion: new Date().toISOString().split('.')[0], // Elimina los milisegundos
          cfFechaLimite: result.cfFechaLimite,
          cfFechaFinalizacion: new Date().toISOString().split('.')[0], // Elimina los milisegundos
          cnUsuarioCreador: this.idUsuarioLogueado,
          cnUsuarioAsignado: null
        };

        console.log(nuevaTarea);
        
        this.taskService.createTask(nuevaTarea).subscribe({
          next: (response) => {
            this.dialog.open(ConfirmationDialog, {
              data: {
                title: "Creacion exitosa.",
                body: "Su tarea se agrego correctamente",
                successButton: "aceptar",
                rejectButton: ""
              }
            });
            this.cargar_tareas();
          },
          error: (err) => {
            console.error('Error al crear tarea:', err);
            this.dialog.open(ConfirmationDialog, {
              data: {
                title: "Error de creacion.",
                body: "Ocurrio un error al crear la tarea",
                successButton: "aceptar",
                rejectButton: ""
              }
            });
          }
        });
      }
    });
  }

  updateTask(){
    if (this.selectedTaskId === 0) {  
      this.dialog.open(ConfirmationDialog, {
        data: {
          title: "¡Cuidado!",
          body: "Debes seleccionar una tarea",
          successButton: "Aceptar",
          rejectButton: ""
        }
      });

      return;
    }

    const task = this.tasks.find(task => task.cnIdTarea === this.selectedTaskId);

    const dialogRef = this.dialog.open(TaskForm, {
      data: {
        ctTituloTarea: task!.ctTituloTarea,
        ctDescripcionTarea: task!.ctDescripcionTarea,
        cnIdComplejidad: task!.cnIdComplejidad,
        cnIdPrioridad: task!.cnIdPrioridad,
        cnNumeroGis: task!.cnNumeroGis,
        cfFechaLimite: task!.cfFechaLimite
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);
        
        this.taskService.updateTask(this.selectedTaskId, result).subscribe({
          next: () => {
            this.dialog.open(ConfirmationDialog, {
              data: {
                title: "Actualizar tarea.",
                body: "La tarea se actualizo correctamente",
                successButton: "aceptar",
                rejectButton: ""
              }
            });
            this.cargar_tareas();
          },
          error: (err) => {
            console.log("Ocurrio un erro al actualizar la tarea", err);
            this.dialog.open(ConfirmationDialog, {
              data: {
                title: "Error al actualizar",
                body: "Ocurrio un error al actualizar la informacion de la tarea",
                successButton: "Aceptar",
                rejectButton: ""
              }
            });
          }
        });
      }
    });
  }

  deleteTask(){
    if (this.selectedTaskId === 0) {  
      this.dialog.open(ConfirmationDialog, {
        data: {
          title: "¡Cuidado!",
          body: "Debes seleccionar una tarea",
          successButton: "Aceptar",
          rejectButton: ""
        }
      });

      return;
    }

    const dialogRef = this.dialog.open(ConfirmationDialog, {
      data: {
        title: "Eliminar tarea",
        body: "¿Seguro que desea eliminar la tarea?",
        successButton: "Eliminar",
        rejectButton: "Cancelar"
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {

        const changeData: IChangeStateTask = {
          cnIdTarea: this.selectedTaskId,
          cnIdUsuario: 3,
          cnIdEstado: 9,
          additionalData: null
        };

        this.taskService.changeTaskState(changeData).subscribe({
          next: (response) => {
            console.log("Tarea eliminada:", response);
            this.cargar_tareas();
          },
          error: (err) => {
            console.error("Error eliminando la tarea:", err);
            alert("Ocurrió un error eliminando la tarea");
          }
        });

      }
    })
  }

  async cambioEstado(task: ITask, event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    const nuevoEstado = selectElement.value;

    console.log("El idTarea es: ", task.cnIdTarea);
    console.log("El nuevo estado es:", nuevoEstado);

    //Para evitar errores
    if (Number(nuevoEstado) === task.cnIdEstado) {
      return;
    }

    switch (nuevoEstado) {
      case "2":
        if(!await this.asignarTarea(task, nuevoEstado))
          selectElement.value = String(task.cnIdEstado);
        break;

      case "4":
        this.tareaEnEspera();
        break;
      case "6":
        this.rechazarTarea();
        break;
      default:
        break;
    }
  }

  async asignarTarea(task: ITask, nuevoEstado: string): Promise<boolean> {
    const dialogRef = this.dialog.open(AsignarForm);

    const idUser: number | undefined = await firstValueFrom(dialogRef.afterClosed());

    console.log("El nombreUsuario es: ", idUser);

    if (idUser === undefined) {
      return false;
    }

    const data: IChangeStateTask = {
      cnIdTarea: task.cnIdTarea,
      cnIdUsuario: this.idUsuarioLogueado,
      cnIdEstado: Number(nuevoEstado),
      additionalData: String(idUser)
    };

    try {
      await firstValueFrom(this.taskService.changeTaskState(data));
      this.cargar_tareas(); // esto puede o no ser await, según su definición
      return true;
    } catch (err) {
      console.error(err);
      return false;
    }

    console.log(data);
    
    return true
  }

  tareaEnEspera(){
    alert("Poner tarea en espera");
  }

  rechazarTarea(){
    alert("rechazar tarea");
  }

  //Funciones auxiliares
  puedeModificarEstado(task: ITask): boolean {
    const estado = task.cnIdEstado;
    const esCreador = task.cnUsuarioCreador === this.idUsuarioLogueado;
    const esAsignado = task.cnUsuarioAsignado === this.idUsuarioLogueado;

    const estadosCreador = [1, 5, 6, 7, 8];
    const estadosAsignado = [2, 3, 4];

    if (estadosCreador.includes(estado)) {
      return esCreador;
    }

    if (estadosAsignado.includes(estado)) {
      return esAsignado;
    }

    return false;
  }
}
