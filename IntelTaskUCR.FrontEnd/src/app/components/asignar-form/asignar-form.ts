import { Component, inject, Inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatRadioModule } from '@angular/material/radio';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { AfterViewInit, ViewChild } from '@angular/core';
import { Userservice } from '../../services/userservice/userservice';
// import {  } from '@angular/core';

interface IUser {
  cnIdUsuario: number;
  ctNombreUsuario: string;
  ctCorreoUsuario: string;
  cfFechaNacimiento: string;
  ctContrasenna: string | null;
  cbEstadoUsuario: boolean;
  cfFechaCreacionUsuario: string | null;
  cfFechaModificacionUsuario: string | null;
  cnIdRol: number;
}

@Component({
  selector: 'app-asignar-form',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, MatDialogModule, MatButtonModule,
    MatFormFieldModule, MatInputModule, MatTableModule, MatRadioModule, MatPaginatorModule],
  templateUrl: './asignar-form.html',
  styleUrl: './asignar-form.css'
})

export class AsignarForm implements AfterViewInit, OnInit {
  form: FormGroup;
  displayedColumns: string[] = ['select', 'nombre', 'correo'];
  dataSource = new MatTableDataSource<IUser>();
  userSeleccionado?: IUser;
  usuarios: IUser[] = [];
  userService = inject(Userservice);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private dialogRef: MatDialogRef<AsignarForm>,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.form = this.fb.group({
      nombre: [''],
      correo: ['']
    });

    this.dataSource.data = this.usuarios;

    this.form.valueChanges.subscribe(filters => {
      this.filtrarUsuarios(filters);
    });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {
    this.userService.readUsers().subscribe((usuarios: IUser[]) => {
    this.usuarios = usuarios;
    this.dataSource.data = usuarios;
  });
  }

  filtrarUsuarios(filters: any): void {
    const { nombre, correo } = filters;

    this.dataSource.data = this.usuarios.filter(user => {
      const matchNombre = user.ctNombreUsuario.toLowerCase().includes(nombre.toLowerCase());
      const matchCorreo = user.ctCorreoUsuario.toLowerCase().includes(correo.toLowerCase());
      return matchNombre && matchCorreo;
    });
  }

  selectUser(user: IUser): void {
    this.userSeleccionado = user;
  }
}
