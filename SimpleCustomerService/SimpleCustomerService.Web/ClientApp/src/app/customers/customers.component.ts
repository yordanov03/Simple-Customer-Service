import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/core/data.service';
import { ICustomer } from 'src/app/interfaces/ICustomer';
import { IPageResult} from 'src/app/interfaces/IPageResult';
import { FilterTextboxComponent} from '../shared/filter-textbox.component'
import { DataFilterService } from '../core/data-filter.service'
import { CustomersGridComponent } from './customers-grid/customers-grid.component';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {

  customers: ICustomer[] = [];
  filteredCustomers: ICustomer[] = [];
  pageSize: number = 10;
  totalRecords: number = 0;

  constructor(private dataService: DataService,
    private dataFilter: DataFilterService,
    private filterTexbox: FilterTextboxComponent,
    private customersGrid: CustomersGridComponent
    ) { }

  ngOnInit() {
    this.getCustomersPage(1);
  }

  filterChanged(filterText: string) {
    if (filterText && this.customers) {
        let props = ['firstName', 'lastName', 'address', 'city', 'state.name', 'orderTotal'];
        this.filteredCustomers = this.dataFilter.filter(this.customers, props, filterText);
    }
    else {
      this.filteredCustomers = this.customers;
    }
  }

  getCusotmers(){
    this.dataService.getCustomers().subscribe(
      (customers:ICustomer[]) =>{
        this.customers = this.filteredCustomers = customers;
      }
      ), ()=> console.log('getCustomers() called')
  }
  pageChanged(page: number) {
    this.getCustomersPage(page);
  }
  
  getCustomersPage(page: number) {
    this.dataService.getCustomersPage((page - 1) * this.pageSize, this.pageSize)
        .subscribe((response: IPageResult<ICustomer[]>) => {
          this.customers = this.filteredCustomers = response.collection;
          this.totalRecords = response.totalRecords;
        },
        (err: any) => console.log(err),
        () => console.log('getCustomersPage() retrieved customers'));
  }

}
