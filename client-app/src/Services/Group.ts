import GroupModel from "@Models/Group";

import BaseService from "./Base";

class GroupService extends BaseService {
  getGroups(pageNumber: number, pageSize: number) {
    return this.requests.get<Array<GroupModel>>("instructor/groups", { pageNumber, pageSize });
  }
}

const groupService = new GroupService();

export default groupService;
