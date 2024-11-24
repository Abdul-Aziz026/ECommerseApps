import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Product } from '../../shared/models/product';


@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'http://localhost:4201/api/'
  private http = inject(HttpClient);

  brands: string[] = [];
  types: string[] = [];

  getProducts(brands?: string[], types?: string[]) {
    let params = new HttpParams();

    if (brands && brands.length > 0) {
      let brand: string = '';
      for (let b of brands) {
        brand = b;
      }
      params = params.set('brand', brand);
      // params.append('brands', brands.join(','));
    }
    if (types && types.length > 0) {
      let type: string = '';
      for (let t of types) {
        type = t;
      }
      // params.append('brands', brands.join(','));
      params = params.set('type', type);
      // params.append('types', types.join(','));
    }

    return this.http.get<any>(this.baseUrl + 'products', {params});
  }

  getProduct(id: number) {
    console.log("ServiceLayer: " + id);
    return this.http.get<any> (this.baseUrl + 'products/' + id);
  }

  getBrands() {
    this.http.get<string[]>(this.baseUrl + 'brands').subscribe({
      next: response => this.brands = response,
      error: error => console.log(error),
      complete: () => console.log("Complete")
    });
    return this.brands;
  }

  getTypes() {
    this.http.get<string[]>(this.baseUrl + 'types').subscribe({
      next: response => this.types = response,
      error: error => console.log(error),
      complete: () => console.log("Complete")
    });
    return this.types;
  }
}
