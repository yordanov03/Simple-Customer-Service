import { ICustomer } from "./ICustomer";

export interface ICustomerResponse {
    status: boolean;
    customer: ICustomer;
}