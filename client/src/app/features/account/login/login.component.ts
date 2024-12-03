import { Component, inject } from '@angular/core';
import { AccountService } from '../../../core/services/account.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  private accountService = inject(AccountService);
  router = inject(Router);

  loginData = {
    username: '',
    password: '',
  };

  onSubmit() {
    if (this.loginData.username && this.loginData.password) {
      this.accountService.login(this.loginData).subscribe({
        next: (user) => {
          if (user) {
            this.accountService.setCurrentUser(user);
            console.log(user);
            alert('Login successfull:');
            this.router.navigateByUrl('/');
          }
        },
        error: (error) => {
          alert('Login failed:');
          this.router.navigateByUrl('account/login');
        }
    });
    }
  }
}
