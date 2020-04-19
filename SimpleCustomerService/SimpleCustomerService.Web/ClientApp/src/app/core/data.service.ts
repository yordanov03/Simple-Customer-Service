import { Observable,  } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ICustomer } from '../interfaces/ICustomer';
import { IState } from '../interfaces/IState';
import { ICustomerResponse } from '../interfaces/ICustomerResponse';
import { IPageResult } from '../interfaces/IPageResult';

@Injectable({
    providedIn: 'root'
})

export class DataService {
    baseUrl = environment.apiUrl;
    baseCustomersUrl = this.baseUrl + 'customers';
    baseStatesUrl = this.baseUrl + 'states'

    constructor(private http: HttpClient) { }

    getCustomers():Observable<ICustomer[]>{
        return this.http.get<ICustomer[]>(this.baseCustomersUrl)
        .pipe(
            map(customers => {
                this.calculateCustomersOrderTotal(customers);
                return customers;
            }),
            catchError(this.handleError)
         );
    }

    getCustomersPage(page: number, pageSize: number):Observable<IPageResult<ICustomer[]>>{
        return this.http.get<ICustomer[]>(`${this.baseUrl}/page/${page}/${pageSize}`, {observe: 'response'}).pipe(
            map((res)=>{
                let totalRecords = + res.headers.get('x-inlinecount');
                let customers = res.body as ICustomer[];
                this.calculateCustomersOrderTotal(customers);
                return {
                    collection: customers,
                    totalRecords: totalRecords
                }
            })
        )
    }

    getCustomer(id: string) : Observable<ICustomer> {
        return this.http.get<ICustomer>(this.baseCustomersUrl + '/' + id)
    }

    getStates() :Observable<IState[]>{
        return this.http.get<IState[]>(this.baseStatesUrl)
        .pipe(
            catchError(this.handleError)
        );
    }

    insertCustomer(customer: ICustomer) : Observable<ICustomer> {
        return this.http.post<ICustomer>(this.baseCustomersUrl, customer);
           
    }
   
    updateCustomer(customer: ICustomer) : Observable<ICustomer> {
        return this.http.put<ICustomer>(this.baseCustomersUrl + '/' + customer.id, customer) 

    }

    deleteCustomer(id: string): Observable<boolean>{
        return this.http.delete<boolean>(this.baseCustomersUrl+ '/' + id)

    }

    calculateCustomersOrderTotal(customers: ICustomer[]) {
        for (let customer of customers) {
            if (customer && customer.orders) {
                let total = 0;
                for (let order of customer.orders) {
                    total += (order.price * order.quantity);
                }
                customer.orderTotal = total;
            }
        }
    }

    private handleError(error: HttpErrorResponse) {
        console.error('server error:', error); 
        if (error.error instanceof Error) {
          let errMessage = error.error.message;
          return Observable.throw(errMessage);
        }
        return Observable.throw(error || 'Angular server error');
    }
}