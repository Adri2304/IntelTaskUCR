import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TaskService } from '../../services/TaskService/task-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-task-detail',
  imports: [CommonModule],
  templateUrl: './task-detail.html',
  styleUrl: './task-detail.css'
})
export class TaskDetail implements OnInit {
  tarea: any;
  usuarios: any[] = [];
  bitacora: any[] = [];
  justificaciones: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private taskService: TaskService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.loadData(id);
    }
  }

  async loadData(id: number) {
    const result = await this.taskService.getAllInfo(id).subscribe({
      next: (data: any) => {
        if (data) {
          this.tarea = data.tarea;
        this.usuarios = data.usuarios;
        this.bitacora = data.bitacora;
        this.justificaciones = data.justificaciones;
        }
      },
      error: (err) => {
        alert("Ocurrio un error al cargar los detalles");
      }
    });
  }
}
