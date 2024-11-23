import { Component, Input } from '@angular/core';
import { Product } from '../../../shared/models/product';

@Component({
  selector: 'app-product-item',
  imports: [],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css'
})
export class ProductItemComponent {
  @Input() product? : Product;
}
