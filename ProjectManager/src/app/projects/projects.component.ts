import { Component, OnInit } from '@angular/core';

import { ProjectService } from '../Service/project.service'
@Component({
    selector: 'app-projects',
    templateUrl: './projects.component.html',
    styleUrls: ['./projects.component.css'],
    providers: [ProjectService]
})
export class ProjectsComponent implements OnInit {

    constructor(private ProjectService: ProjectService) { }

    ngOnInit() {
    }

}
