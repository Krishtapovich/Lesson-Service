import GroupModel from "@Models/Group";

import BaseService from "./Base";

class GroupService extends BaseService {
  getGroups(pageNumber: number, pageSize: number) {
    return this.requests.get<Array<GroupModel>>("group/groups-list", { pageNumber, pageSize });
  }

  getGroupsNumbers() {
    return this.requests.get<Array<string>>("group/groups-numbers");
  }
}

const groupService = new GroupService();

export default groupService;
