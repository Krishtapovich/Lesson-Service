import GroupIcon from "@mui/icons-material/Group";
import QuizIcon from "@mui/icons-material/Quiz";
import { ListItem, ListItemIcon, ListItemText } from "@mui/material";
import Box from "@mui/material/Box";
import Drawer from "@mui/material/Drawer";
import List from "@mui/material/List";
import { NavLink, useLocation } from "react-router-dom";

const routes = [
  {
    to: "/surveys",
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

  const isActive = (path: string) => pathname === path;
  const color = (path: string) => (isActive(path) ? "primary.main" : "primary.light");

  return (
    <Box sx={{ display: "flex" }}>
      <Drawer
        PaperProps={{
          sx: {
            width: 230,
            backgroundColor: "primary.dark"
          }
        }}
        variant="permanent"
        anchor="left"
      >
        <List>
          {routes.map((route) => (
            <>
              <NavLink to={route.to} style={{ textDecoration: "none" }}>
                <ListItem
                  button
                  sx={{
                    backgroundColor: isActive(route.to) ? "rgba(255,255,255, 0.08)" : "none",
                    color: color(route.to),
                    borderRadius: 1,
                    width: "90%",
                    margin: "auto",
                    marginBottom: 1,
                    "&:hover": {
                      backgroundColor: "rgba(255,255,255, 0.08)"
                    }
                  }}
                >
                  <ListItemIcon sx={{ color: color(route.to) }}>{route.icon}</ListItemIcon>
                  <ListItemText sx={{ color: color(route.to) }}>{route.text}</ListItemText>
                </ListItem>
              </NavLink>
            </>
          ))}
        </List>
      </Drawer>
      <Box component="main" sx={{ width: "calc(100% - 230px)", marginLeft: "230px" }}>
        {props.children}
      </Box>
    </Box>
  );
}
