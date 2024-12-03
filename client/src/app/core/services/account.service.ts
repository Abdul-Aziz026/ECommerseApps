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

  register(values: any) {
    return this.http.post(this.baseUrl + 'account/register', values);
  }

  login(values: any) {
    return this.http.post<User>(`${this.baseUrl}account/login`, values);
  }
  
  public setCurrentUser(user: User) {
    this.currentUser.set(user);
    localStorage.setItem('user', JSON.stringify(user)); // Store user data in local storage
  }

  logout() {
    console.log("Come to service layer...");
    return this.http.post(this.baseUrl + 'account/logout', {});
    // this.currentUser.set(null);
    // localStorage.removeItem('user'); // Clear user data on logout
  }

}
