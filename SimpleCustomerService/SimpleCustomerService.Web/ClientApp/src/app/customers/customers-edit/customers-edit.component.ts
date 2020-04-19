import { Component, OnInit, NgModule } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms'
import { DataService } from '../../core/data.service'
import { ICustomer } from 'src/app/interfaces/ICustomer';
import { IState } from 'src/app/interfaces/IState';
import { ActivatedRoute, Router } from '@angular/router';
import { ValueConverter } from '@angular/compiler/src/render3/view/template';

@Component({
  selector: 'app-customers-edit',
  templateUrl: './customers-edit.component.html',
  styleUrls: ['./customers-edit.component.css']
})


export class CustomersEditComponent implements OnInit {

  customerForm: FormGroup;
  customer: ICustomer = {
    firstName: '',
    lastName: '',
    gender: '',
    address: '',
    email: '',
    city: '',
    stateId: 0,
    zip: 0
  };
  states: IState[];
  operationText: string = 'Insert';
  errorMessage: string;
  deleteMessageEnabled: boolean;

  constructor(private dataService: DataService, 
    private formBuilder: FormBuilder, 
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    let id = this.route.snapshot.params['id'];
    if (id !== '0') {
      this.operationText = 'Update';
      this.getCustomer(id);
    }

    this.getStates();
    this.buildForm();
  }

  getCustomer(id: string){
    this.dataService.getCustomer(id).subscribe(
      (customer:ICustomer)=>{
        this.customer = customer;
        this.buildForm();
      }
    )
  }

  getStates(){
    this.dataService.getStates().subscribe(
      (states: IState[])=>{
        this.states = states
      }
    )
  }

  buildForm(){
    this.customerForm=this.formBuilder.group({
      firstName:  [this.customer.firstName, Validators.required],
      lastName:   [this.customer.lastName, Validators.required],
        gender:     [this.customer.gender, Validators.required],
        email:      [this.customer.email, Validators.required],
        address:    [this.customer.address, Validators.required],
        city:       [this.customer.city, Validators.required],
        stateId:    [this.customer.stateId, Validators.required]
    })
  }

  deleteCustomer(event: Event) {
    event.preventDefault();
    this.dataService.deleteCustomer(this.customer.id).subscribe((status: boolean)=>{
      
      if(status){
        this.router.navigate(['/customers'])
      }

      else{
        this.errorMessage = 'Unable to delete customer'
      }
    }, (err)=> console.log(err))
  }

  cancel(event: Event) {
    event.preventDefault();
    this.router.navigate(['/customers']);
  }


// from the formgroup object value containig customer info
  submit({value} : {value:ICustomer}){

    value.id = this.customer.id;
    value.zip = this.customer.zip || 0; 

    if(value.id){
      this.dataService.updateCustomer(value).subscribe((customer:ICustomer)=>{
        if(customer){
          this.router.navigate(['/customers'])
        }
        else{
          this.errorMessage = 'Unable to update customer!'
        }
      })
    }

    else{
      this.dataService.insertCustomer(value).subscribe((customer:ICustomer)=>{

        if(customer){
          this.router.navigate(['/customers'])
        }

        else{
            this.errorMessage= 'Unable to add customer!'
        }
      })
    }
  }
}
