import { Component } from '@angular/core';
import { Toolbar } from './shared/toolbar/toolbar';
import { Sidenav } from './shared/sidenav/sidenav';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [Toolbar, Sidenav],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'IntelTaskUCR.FrontEnd';
}
