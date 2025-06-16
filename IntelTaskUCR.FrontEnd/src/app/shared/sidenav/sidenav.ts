import { Component } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-sidenav',
  imports: [MatSidenavModule, RouterOutlet],
  templateUrl: './sidenav.html',
  styleUrl: './sidenav.css'
})
export class Sidenav {

}
