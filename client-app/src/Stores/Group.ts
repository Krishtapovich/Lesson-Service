import GroupModel from "@Models/Group";
import groupService from "@Services/Group";
import { makeAutoObservable, runInAction } from "mobx";

export default class GroupStore {
  groups: Array<GroupModel> = [];
  groupsNumbers: Array<string> = [];

  constructor() {
    makeAutoObservable(this);
  }

  dispose() {
    this.groups = [];
  }

  async init() {
    const groups = await groupService.getGroups(1, 1);
    runInAction(() => (this.groups = groups));
  }

  async getGroups(pageNumber: number, pageSize: number) {
    const groups = await groupService.getGroups(pageNumber, pageSize);
    runInAction(() => this.groups.concat(groups));
  }

  async getGroupsNumbers() {
    const groupsNumbers = await groupService.getGroupsNumbers();
    runInAction(() => (this.groupsNumbers = groupsNumbers));
  }
}
