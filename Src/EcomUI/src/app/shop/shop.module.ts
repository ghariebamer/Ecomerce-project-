import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop/shop.component';
import { ShopItemsComponent } from './shop-items/shop-items.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    ShopComponent,
    ShopItemsComponent,
  ],
  imports: [
    CommonModule,
    PaginationModule,
    SharedModule
  ],
  exports:[ShopComponent]
})
export class ShopModule { }
