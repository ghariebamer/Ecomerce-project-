import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../../interfaces/Models/Products';

@Component({
  selector: 'app-shop-items',
  templateUrl: './shop-items.component.html',
  styleUrl: './shop-items.component.css'
})
export class ShopItemsComponent implements OnInit {
 @Input() product!:Product

  constructor() {
  }
  ngOnInit()  {

  }

}
