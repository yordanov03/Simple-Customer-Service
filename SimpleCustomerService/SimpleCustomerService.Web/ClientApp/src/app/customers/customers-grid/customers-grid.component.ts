import { Component, OnInit, Input } from '@angular/core';
import { ICustomer } from 'src/app/interfaces/ICustomer';
import { Sorter } from '../../core/sorter'

@Component({
  selector: 'app-customers-grid',
  templateUrl: './customers-grid.component.html',
  styleUrls: ['./customers-grid.component.css']
})
export class CustomersGridComponent implements OnInit {

  @Input() customers: ICustomer[] = [];

  constructor(private sorter: Sorter) { }

  ngOnInit() {
  }

  sort(prop: string) {
    this.sorter.sort(this.customers, prop);
}
}
