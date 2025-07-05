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
import { EnEsperaForm } from '../en-espera-form/en-espera-form';
import { Authservice } from '../../services/AuthService/authservice';
import { Router, RouterLink } from '@angular/router';
import { IStates } from '../../models/istates';
import { IFilterTask } from '../../models/ifilter-task';
import { IComplexities } from '../../models/icomplexities';
import { IPriorities } from '../../models/ipriorities';

@Component({
  selector: 'app-task',
  imports: [MatButtonModule, MatCardModule, MatIconModule, MatFormFieldModule, MatSelectModule, MatCheckboxModule,
  CommonModule, FormsModule, RouterLink],
  templateUrl: './task.html',
  styleUrl: './task.css'
})
export class Task implements OnInit{
  authService = inject(Authservice);
  taskService: TaskService = inject(TaskService)
  stateService = inject(Stateservice);
  dialog = inject(MatDialog);
  router = inject(Router);

  idUsuarioLogueado = 0;
  idRolUsuarioLogueado = 0;
  selectedTaskId: number = 0;
  tasks: ITask[] = [];
  selectedStates: number[] = [];
  nombresEstados: { [id: number]: string } = {};
  nombresComplejidades: { [id: number]: string } = {};
  nombresPrioridades: { [id: number]: string } = {};

  ngOnInit(): void {
    const usuario = this.authService.obtenerUsuario();

    if (!usuario) {
      console.warn('Usuario no autenticado');
      this.router.navigate(["/login"]);
      return;
    }

    this.idUsuarioLogueado = usuario.cnIdUsuario;
    this.idRolUsuarioLogueado = usuario.cnIdRol;    

    this.cargarEstados();
    this.cargarComplejidades();
    this.cargarPrioridades();
    
    //Suscribirse al servicio de los estados del sidenav
    this.stateService.selectedIds$.subscribe((ids) => {
      this.selectedStates = ids;
      console.log('IDs actualizados desde sidenav:', ids);
      this.cargar_tareas();
    });
    
  }

  onCheckboxChange(taskId: number) {
  this.selectedTaskId = this.selectedTaskId === taskId ? 0 : taskId;
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
          // cfFechaAsignaion: new Date().toISOString().split('.')[0], 
          cfFechaAsignacion: this.formatearFechaLocal(new Date()),
          cfFechaFinalizacion: this.formatearFechaLocal(new Date()),
          cfFechaLimite: result.cfFechaLimite,
          // cfFechaFinalizacion: new Date().toISOString().split('.')[0], // Elimina los milisegundos
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

    if (task.cnIdEstado === 6 && nuevoEstado === "2") {
      if(!await this.cambioEstadoNormal(task, nuevoEstado)){}
        selectElement.value = String(task.cnIdEstado);
      return;
    }

    // if (nuevoEstado === "3") {
    //   const existeEstado3 = this.tasks.some(t => t.cnIdEstado === 3);
    //   if (existeEstado3) {
    //     // Aquí ya existe una tarea con estado 3
    //     // Puedes mostrar un mensaje o cancelar el cambio
    //     alert("Ya existe una tarea con estado 3. No se puede asignar esta tarea a ese estado.");
    //     selectElement.value = String(task.cnIdEstado);
    //     return;
    //   }
    // }

    switch (nuevoEstado) {
      case "2":
        if(!await this.asignarTarea(task, nuevoEstado))
          selectElement.value = String(task.cnIdEstado);
        break;

      case "4":
        if(!await this.tareaEnEspera(task, nuevoEstado))
          selectElement.value = String(task.cnIdEstado);
        break;
      case "6":
        if(!await this.rechazarTarea(task, nuevoEstado))
          selectElement.value = String(task.cnIdEstado);
        break;
      default:
        if(!await this.cambioEstadoNormal(task, nuevoEstado))
          selectElement.value = String(task.cnIdEstado);
        break;
    }
  }

  async cambioEstadoNormal(task: ITask, nuevoEstado: string): Promise<boolean>{
    const data: IChangeStateTask = {
      cnIdTarea: task.cnIdTarea,
      cnIdUsuario: this.idUsuarioLogueado,
      cnIdEstado: Number(nuevoEstado),
      additionalData: null
    };

    try {
      await firstValueFrom(this.taskService.changeTaskState(data));
      this.cargar_tareas(); // esto puede o no ser await, según su definición
      return true;
    } catch (err) {
      console.error(err);
      return false;
    }
  }

  async asignarTarea(task: ITask, nuevoEstado: string): Promise<boolean> {
    const dialogRef = this.dialog.open(AsignarForm);
    const idUser: number | undefined = await firstValueFrom(dialogRef.afterClosed());

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
  }

  async tareaEnEspera(task: ITask, nuevoEstado: string){
    const dialogRef = this.dialog.open(EnEsperaForm);
    const mensaje: string = await firstValueFrom(dialogRef.afterClosed());


    if (!mensaje) {
      return false;
    }

    const data: IChangeStateTask = {
      cnIdTarea: task.cnIdTarea,
      cnIdUsuario: this.idUsuarioLogueado,
      cnIdEstado: Number(nuevoEstado),
      additionalData: mensaje
    };

    try {
      await firstValueFrom(this.taskService.changeTaskState(data));
      this.cargar_tareas();
      return true;
    } catch (err) {
      console.error(err);
      return false;
    }
  }

  async rechazarTarea(task: ITask, nuevoEstado: string){
    const dialogRef = this.dialog.open(EnEsperaForm);
    const mensaje: string = await firstValueFrom(dialogRef.afterClosed());


    if (!mensaje) {
      return false;
    }

    const data: IChangeStateTask = {
      cnIdTarea: task.cnIdTarea,
      cnIdUsuario: this.idUsuarioLogueado,
      cnIdEstado: Number(nuevoEstado),
      additionalData: mensaje
    };

    try {
      await firstValueFrom(this.taskService.changeTaskState(data));
      this.cargar_tareas();
      return true;
    } catch (err) {
      console.error(err);
      return false;
    }
  }

  //Funciones auxiliares
  cargar_tareas() {
    const body: IFilterTask = {
      states: this.selectedStates,
      descending: true
    }

    this.taskService.filterTaskPerUser(this.idUsuarioLogueado, body).subscribe({
      next: (data) => {
        if (data === null) {
          this.tasks = [];
          return;
        }

        // convertir fechas de las tareas
        const tareas: ITask[] = data.map((task: ITask) => ({
          ...task,
          cfFechaAsignacion: task.cfFechaAsignacion ? new Date(task.cfFechaAsignacion) : null,
          cfFechaLimite: new Date(task.cfFechaLimite),
          cfFechaFinalizacion: task.cfFechaFinalizacion ? new Date(task.cfFechaFinalizacion) : null,
        }));
        
        // crear observables para estados posteriores
        const estadosObservables = tareas.map((task) =>
          this.stateService.getEstadosPosteriores(task.cnIdEstado)
        );

        // esperar todas las respuestas
        forkJoin(estadosObservables).subscribe({
          next: (estadosPorTarea: number[][]) => {
            this.tasks = tareas.map((task, i) => ({
              ...task,
              estadosDisponibles: estadosPorTarea[i],
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

  cargarEstados(){
    this.stateService.getAllStates().subscribe({
      next: (res: IStates[]) => {
        res.forEach(element => {
          this.nombresEstados[element.cnIdEstado] = element.ctEstado
        });
      },
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

  cargarComplejidades(){
    this.stateService.getAllComplexities().subscribe({
      next: (res: IComplexities[]) => {
        res.forEach(element => {
          this.nombresComplejidades[element.cnIdComplejidad] = element.ctNombre
        });
      },
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

  cargarPrioridades(){
    this.stateService.getAllPriorities().subscribe({
      next: (res: IPriorities[]) => {
        res.forEach(element => {
          this.nombresPrioridades[element.cnIdPrioridad] = element.ctNombrePrioridad
        });
      },
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

  puedeModificarEstado(task: ITask): boolean {
    const estado = task.cnIdEstado;
    const esCreador = task.cnUsuarioCreador === this.idUsuarioLogueado;
    const esAsignado = task.cnUsuarioAsignado === this.idUsuarioLogueado;

    const estadosCreador = [1, 5, 6, 8];
    const estadosAsignado = [2, 3, 4];

    if (estadosCreador.includes(estado)) {
      return esCreador;
    }

    if (estadosAsignado.includes(estado)) {
      return esAsignado;
    }

    return false;
  }

  private formatearFechaLocal(fecha: Date): string {
    const yyyy = fecha.getFullYear();
    const MM = String(fecha.getMonth() + 1).padStart(2, '0');
    const dd = String(fecha.getDate()).padStart(2, '0');
    const hh = String(fecha.getHours()).padStart(2, '0');
    const mm = String(fecha.getMinutes()).padStart(2, '0');
    const ss = String(fecha.getSeconds()).padStart(2, '0');
    return `${yyyy}-${MM}-${dd}T${hh}:${mm}:${ss}`;
  }

}
