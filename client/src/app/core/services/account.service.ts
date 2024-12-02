import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  currentUser = signal<User | null>(null);

  login(values: any) {
    return this.http.post<User>(`${this.baseUrl}account/login`, values).subscribe({
      next: (user) => {
        if (user) {
          this.setCurrentUser(user);
        }
      },
      error: (error) => {
        console.error('Login failed:', error);
      }
    });
  }
  
  private setCurrentUser(user: User) {
    this.currentUser.set(user);
    localStorage.setItem('user', JSON.stringify(user)); // Store user data in local storage
  }

  logout() {
    this.currentUser.set(null);
    localStorage.removeItem('user'); // Clear user data on logout
  }

}
