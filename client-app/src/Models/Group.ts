import StudentModel from "./Student";

export default interface GroupModel {
  id: number;
  number: number;
  students: Array<StudentModel>;
}
