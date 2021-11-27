import GroupIcon from "@mui/icons-material/Group";
import QuizIcon from "@mui/icons-material/Quiz";
import { ListItem, ListItemIcon, ListItemText } from "@mui/material";
import Box from "@mui/material/Box";
import Drawer from "@mui/material/Drawer";
import List from "@mui/material/List";
import { NavLink, useLocation } from "react-router-dom";

import { color, content, drawer, listItem, navLink } from "./style";

const routes = [
  {
    to: "/",
    icon: <QuizIcon />,
    text: "Surveys"
  },
  {
    to: "/students",
    icon: <GroupIcon />,
    text: "Students"
  }
];

export default function Layout(props: React.PropsWithChildren<{}>) {
  const { pathname } = useLocation();

  return (
    <Box>
      <Drawer PaperProps={{ sx: drawer }} variant="permanent" anchor="left">
        <List>
          {routes.map((route) => (
            <NavLink to={route.to} style={navLink} key={route.to}>
              <ListItem button sx={listItem(pathname === route.to)}>
                <ListItemIcon sx={color(pathname === route.to)}>{route.icon}</ListItemIcon>
                <ListItemText sx={color(pathname === route.to)}>{route.text}</ListItemText>
              </ListItem>
            </NavLink>
          ))}
        </List>
      </Drawer>
      <Box sx={content}>{props.children}</Box>
    </Box>
  );
}
