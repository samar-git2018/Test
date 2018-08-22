import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'app/Service/project.service';
import { Project } from 'app/Model/project';
import { NgForm } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'app-project',
    templateUrl: './project.component.html',
    styleUrls: ['./project.component.css'],
    providers: [ProjectService]
})
export class ProjectComponent implements OnInit {
    constructor(private projectService: ProjectService, private datePipe: DatePipe) { }

    ngOnInit() {
        this.resetForm();
    }

    resetForm(form?: NgForm) {
        if (form != null)
            form.reset();
        this.projectService.selectedProject = {
            ProjectId: 0,
            ProjectName: '',
            Start_Date: null,
            End_Date: null,
            Priority: 0,
            ManagerId: 0,
            SetDate: false
        }
    }
    SetDateRange(e) {
        if (e.target.checked) {
            var date = new Date();
            this.projectService.selectedProject.Start_Date = this.datePipe.transform(date, "yyyy-MM-dd");
            this.projectService.selectedProject.End_Date = this.datePipe.transform(date.setDate(date.getDate() + 1), "yyyy-MM-dd");
        }
        else {
            this.projectService.selectedProject.Start_Date = this.projectService.selectedProject.End_Date = null;
        }
    }

    onSubmit(form: NgForm) {
        if (form.value.ProjectID == null) {
            console.log(form.value);
            this.projectService.postProject(form.value)
                .add(data => {
                    this.resetForm(form);
                    this.projectService.getProjectList();
                    alert('New Project added Succcessfully');
                })
        }
        else {
            this.projectService.putProject(form.value.ProjectID, form.value)
                .add(data => {
                    this.resetForm(form);
                    this.projectService.getProjectList();
                    alert('New Project updated Succcessfully');
                });
        }
    }
}
