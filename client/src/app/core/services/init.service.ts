import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { lastValueFrom, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  cartService = inject(CartService);

  init() {
    console.log("Init() called...");
    const cartId = localStorage.getItem('cart_id');
    const cart$ = cartId ? this.cartService.getCart(cartId) : of(null);

    return cart$;
  }
  async new(){
    try {
      var items = await lastValueFrom(this.init());
      console.log(items);
    }
    catch (error) {
      console.error("Error in initialization:", error);
    }
    const splash = document.getElementById('initial-splash');
    if (splash) {
      splash.remove();
    }
  }
}
