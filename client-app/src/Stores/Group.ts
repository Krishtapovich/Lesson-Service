import StudentModel from "@Models/Student";
import groupService from "@Services/Group";
import { LOAD_TIME } from "@Utils/Theme";
import { makeAutoObservable, runInAction } from "mobx";

export default class GroupStore {
  students: Array<StudentModel> = [];
  groupsNumbers: Array<string> = [];

  isLoading = false;

  constructor() {
    makeAutoObservable(this);
  }

  dispose() {
    this.students = [];
    this.groupsNumbers = [];
  }

  getStudents() {
    this.isLoading = true;
    setTimeout(async () => {
      const students = await groupService.getStudents();
      runInAction(() => {
        this.students = students;
        this.isLoading = false;
      });
    }, LOAD_TIME);
  }

  getGroupsNumbers() {
    this.isLoading = true;
    setTimeout(async () => {
      const groupsNumbers = await groupService.getGroupsNumbers();
      runInAction(() => {
        this.groupsNumbers = groupsNumbers;
        this.isLoading = false;
      });
    }, LOAD_TIME);
  }

  updateStudent(student: StudentModel) {
    this.students = this.students.map((s) => (s.id === student.id ? student : s));
    groupService.updateStudent(student);
  }

  deleteStudent(studentId: number) {
    this.students = this.students.filter((s) => s.id !== studentId);
    groupService.deleteStudent(studentId);
  }
}
