import StudentModel from "@Models/Student";

import BaseService from "./Base";

class GroupService extends BaseService {
  getStudents() {
    return this.requests.get<Array<StudentModel>>("group/students-list");
  }

  getGroupsNumbers() {
    return this.requests.get<Array<string>>("group/groups-numbers");
  }

  updateStudent(student: StudentModel) {
    return this.requests.put<void>("group/update-student", student, {});
  }

  deleteStudent(studentId: number) {
    return this.requests.delete<void>("group/delete-student", { studentId });
  }
}

const groupService = new GroupService();

export default groupService;
