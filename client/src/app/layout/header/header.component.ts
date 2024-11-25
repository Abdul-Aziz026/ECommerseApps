import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BusyService } from '../../core/services/busy.service';
import { MatProgressBar, MatProgressBarModule } from '@angular/material/progress-bar';


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink,
    MatProgressBar
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})

export class HeaderComponent {
  busyService = inject(BusyService);

}
