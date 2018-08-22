"use strict";
var Project = (function () {
    function Project(ProjectId, ProjectName, Start_Date, End_Date, Priority, ManagerId) {
        this.ProjectId = ProjectId;
        this.ProjectName = ProjectName;
        this.Start_Date = Start_Date;
        this.End_Date = End_Date;
        this.Priority = Priority;
        this.ManagerId = ManagerId;
    }
    return Project;
}());
exports.Project = Project;
//# sourceMappingURL=project.js.map