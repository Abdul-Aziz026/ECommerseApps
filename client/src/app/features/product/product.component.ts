import { Component, inject } from '@angular/core';
import { ProductService } from '../../core/services/product.service';
import { FormsModule } from '@angular/forms';
import { addProduct } from '../../shared/models/user';

@Component({
  selector: 'app-product',
  imports: [FormsModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  productService = inject(ProductService);
  product: addProduct = {
    name: '',
    description: '',
    price: 0,
    pictureUrl: '',
    type: '',
    brand: '',
    quantityInStock: 0
  };


  // need to implement...
  onSubmit() {
    console.log("This product: " + this.product);
    this.productService.createProduct(this.product).subscribe({
      next: (response) => {
        console.log('Product created successfully:', response);
        alert('Product created successfully!');
      },
      error: (error) => {
        console.error('Error creating product:', error.message);
        alert('Failed to create product');
      }
    });
  }
}
