import { Component, OnInit } from '@angular/core';
import { UserService } from '../Service/user.service'
@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

    constructor(private userService: UserService) { }

  ngOnInit() {
  }

}
