import { Component } from '@angular/core';
import { Toolbar } from './shared/toolbar/toolbar';
import { Sidenav } from './shared/sidenav/sidenav';
// import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterModule, Router, NavigationEnd, RouterOutlet  } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'IntelTaskUCR.FrontEnd';
  // mostrarLayout = true;

  // constructor(private router: Router) {
  //   this.router.events
  //     .pipe(filter(event => event instanceof NavigationEnd))
  //     .subscribe((event: any) => {
  //       // Oculta todo si la ruta es /login
  //       this.mostrarLayout = !event.url.includes('/login');
  //     });
  // }
}
