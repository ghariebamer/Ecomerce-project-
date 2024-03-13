import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../enviorment';
import { Pagination } from '../interfaces/pagination';
import { ShopParams } from '../interfaces/Models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl=environment.productURl;
  constructor(private http:HttpClient) { }

getproducts(shopParams?:ShopParams){
  debugger;
 let params= new HttpParams();
if(shopParams?.CategoryId){
  params=params.append('CategoryId',shopParams.CategoryId.toString())
}
if(shopParams?.Sort){
  params=params.append('Sort',shopParams?.Sort)
}
if(shopParams){
  params=params.append('PageNumber',shopParams.PageNumber.toString())
  params=params.append('PageSize',shopParams?.PageSize.toString())
}

if(shopParams&&shopParams.Search)
params=params.append('Search',shopParams.Search)

  return this.http.get<Pagination>(this.baseUrl+'/GetAllProducts',{params:params});
}

}
