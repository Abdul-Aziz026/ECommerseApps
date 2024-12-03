import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BusyService } from '../../core/services/busy.service';
import { MatProgressBar, MatProgressBarModule } from '@angular/material/progress-bar';
import { MatIconModule } from '@angular/material/icon';
import { CartService } from '../../core/services/cart.service';
import { AccountService } from '../../core/services/account.service';
import { Router } from '@angular/router';



@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink,
    MatProgressBar,
    MatIconModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})

export class HeaderComponent {
  busyService = inject(BusyService);
  cartService = inject(CartService);
  accountService = inject(AccountService);
  router = inject(Router);


  logout() {
    this.accountService.logout().subscribe({
      next: (response: any) => {
        this.accountService.currentUser.set(null);
        localStorage.removeItem('user'); // Clear user data on logout
        var storeUser = localStorage.getItem('user');
        console.log("Store user: " + storeUser);
        alert(response.message);
        this.router.navigateByUrl('/');
      },
      error: error => {
        alert(error.message);
        this.router.navigateByUrl('');
      }
    });
  }

}
