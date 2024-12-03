import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-checkout',
  imports: [FormsModule],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent {
  shipping = {
    name: '',
    address: '',
    city: '',
    state: '',
    zipcode: '',
    phone: ''
  };

  payment = {
    cardName: '',
    cardNumber: '',
    expiry: '',
    cvv: ''
  };

  // Order details (in a real application, this data would come from a service or state management)
  orderSummary = [
    { name: 'Product 1', price: 20.00 },
    { name: 'Product 2', price: 15.00 }
  ];

  totalAmount = this.orderSummary.reduce((total, item) => total + item.price, 0);


  placeOrder() {
    console.log('Order placed:', {
      shipping: this.shipping,
      payment: this.payment,
      totalAmount: this.totalAmount
    });
    // Call API to process payment and complete order
  }

}
