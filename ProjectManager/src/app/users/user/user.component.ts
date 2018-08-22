import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/Service/user.service';
import { User } from 'app/Model/user';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
    constructor(private userService: UserService) { }

    ngOnInit() {
        this.resetForm();
    }

    resetForm(form?: NgForm) {
        if (form != null)
            form.reset();
        this.userService.selectedUser = {
            User_ID: 0,
            First_Name: '',
            Last_Name: '',
            Employee_ID: '',
            Project_ID: 0,
            Task_ID: 0
        }
    }    

    onSubmit(form: NgForm) {
        if (form.value.User_ID == 0) {
            console.log(form.value);
            this.userService.postUser(form.value)
                .add(data => {
                    this.resetForm(form);
                    this.userService.getUserList();
                    alert('New User added Succcessfully');
                })
        }
        else {
            this.userService.putUser(form.value.User_ID, form.value)
                .add(data => {
                    this.resetForm(form);
                    this.userService.getUserList();
                    alert('New User updated Succcessfully');
                });
        }
    }
}
