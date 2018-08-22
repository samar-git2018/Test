export class Project {
    constructor(
        public ProjectId: number,
        public ProjectName: string,
        public Start_Date: String,
        public End_Date: String,
        public Priority: number,
        public ManagerId: number,
        public SetDate: Boolean
    ) { }
}