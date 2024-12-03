import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { addProduct } from '../../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseUrl = 'http://localhost:4201/api/';
  http = inject(HttpClient);

  createProduct(product: addProduct): Observable<addProduct> {
    return this.http.post<addProduct>(`${this.baseUrl}/addproducts`, product);
  }
}
