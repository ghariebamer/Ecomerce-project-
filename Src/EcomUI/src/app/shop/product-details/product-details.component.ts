import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../interfaces/Models/Products';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {
  ProductData = <Product>{};
  constructor(private _ShopService: ShopService, private activatedRoute: ActivatedRoute) {

  }

  ngOnInit() {
    this.loadProductDetails();
  }
  loadProductDetails() {
    this._ShopService.getProductById(this.activatedRoute.snapshot.paramMap.get('id') ? +this.activatedRoute.snapshot.paramMap.get('id')! : 0).subscribe(res => {
      this.ProductData = res;
    })
  }

}
