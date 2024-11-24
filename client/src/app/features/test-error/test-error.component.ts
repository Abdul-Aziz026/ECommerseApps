import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-test-error',
  imports: [],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.css'
})
export class TestErrorComponent {
  baseUrl = 'http://localhost:4201/'
  private http = inject(HttpClient);
  validationErrors?: string[];


  get404Error() {
    this.http.get(this.baseUrl + 'Buggy/notfound').subscribe({
      next: Response => console.log(Response),
      error: error => console.log(error)
    })
  }

  get400Error() {
    this.http.get(this.baseUrl + 'Buggy/badrequest').subscribe({
      next: Response => console.log(Response),
      error: error => console.log(error)
    })
  }

  
  get401Error() {
    this.http.get(this.baseUrl + 'Buggy/unauthorized').subscribe({
      next: Response => console.log(Response),
      error: error => console.log(error)
    })
  }

  
  get500Error() {
    this.http.get(this.baseUrl + 'Buggy/internalerror').subscribe({
      next: Response => console.log(Response),
      error: error => console.log(error)
    })
  }
  get405ValidationError() {
    this.http.get(this.baseUrl + 'Buggy/validationerror').subscribe({
      next: Response => console.log(Response),
      error: error => {
        // Check if error.errors exists and is an array
        console.log('Error: ' + error.error);
      }
    })
  }

}
