import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { Product } from '../../../shared/models/product';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  imports: [],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit{
  private shopService = inject(ShopService);
  private activatedRoute = inject(ActivatedRoute);
  product?: Product;

  ngOnInit(): void {
    console.log("Hello");
    this.loadProduct();
  }

  loadProduct() {
    let id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;
    this.shopService.getProduct(id).subscribe({
      next: Response => this.product = Response,
      error: error => console.log(error.message),
      complete: () => console.log("Complete Product fetch: " + id)
    })
  }
}
