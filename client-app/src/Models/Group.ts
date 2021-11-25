import StudentModel from "./Student";

export default interface GroupModel {
  id: number;
  number: string;
  students: Array<StudentModel>;
}
