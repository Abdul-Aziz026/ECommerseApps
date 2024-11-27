import { Component, input } from '@angular/core';
import { CartItem } from '../../../shared/models/cart';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-cart-item',
  imports: [RouterLink],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css'
})
export class CartItemComponent {
  item = input.required<CartItem>();
}
