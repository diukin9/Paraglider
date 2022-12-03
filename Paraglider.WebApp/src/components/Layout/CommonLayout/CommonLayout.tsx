import { Outlet } from "react-router-dom";
import { Content } from "../Content";
import { Header } from "../Header";
import { CommonLayoutRoot } from "./CommonLayout.styles";

export const CommonLayout = () => {
  return (
    <CommonLayoutRoot>
      <Header />
      <Content>
        <Outlet />
      </Content>
    </CommonLayoutRoot>
  );
};
