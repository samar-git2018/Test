import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { ProjectService } from './Service/project.service';
import { UserService } from './Service/user.service';
import { DatePipe } from '@angular/common';;
import { ProjectsComponent } from './projects/projects.component';
import { ProjectComponent } from 'app/projects/project/project.component';
import { ProjectListComponent } from 'app/projects/project-list/project-list.component';
import { UsersComponent } from './users/users.component';
import { UserListComponent } from 'app/users/user-list/user-list.component';
import { UserComponent } from './users/user/user.component';
import { FilterPipe } from 'app/users/user-list/filter.pipe';
import { OrderByPipe } from 'app/users/user-list/orderby.pipe';

@NgModule({
    declarations: [
        AppComponent,
        ProjectsComponent,
        ProjectComponent,
        ProjectListComponent,
        UsersComponent,
        UserComponent,
        UserListComponent,
        FilterPipe,
        OrderByPipe],
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: 'users', component: UsersComponent },
            { path: 'projects', component: ProjectsComponent }
        ])
    ],
    providers: [ProjectService, DatePipe, UserService],
    bootstrap: [AppComponent]
})
export class AppModule { }
