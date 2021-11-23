import { createContext, useContext } from "react";

import GroupStore from "./Group";
import SurveyStore from "./Survey";

interface Store {
  surveyStore: SurveyStore;
  groupStore: GroupStore;
}

const store: Store = {
  surveyStore: new SurveyStore(),
  groupStore: new GroupStore()
};

const storeContext = createContext(store);

export default function useStore() {
  return useContext(storeContext);
}
