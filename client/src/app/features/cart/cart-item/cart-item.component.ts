import { Component, inject, input } from '@angular/core';
import { CartItem } from '../../../shared/models/cart';
import { RouterLink } from '@angular/router';
import { MatIcon } from '@angular/material/icon';
import { CartService } from '../../../core/services/cart.service';

@Component({
  selector: 'app-cart-item',
  imports: [RouterLink, MatIcon],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css'
})
export class CartItemComponent {
  item = input.required<CartItem>();
  cartService = inject(CartService);

  incrementQuantity() {
    this.cartService.addItemToCart(this.item());
  }

  
  decrementQuantity() {
    this.cartService.removeItemFromCart(this.item().productId, 1);
  }
  
  removeItemFromCart() {
    console.log("Called remove");
    this.cartService.removeItemFromCart(this.item().productId, this.item().quantity);
    
  }
}
