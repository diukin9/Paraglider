import { Outlet } from "react-router-dom";

import { Content } from "../Content";
import { Header } from "../Header";

export const CommonLayout = () => {
  return (
    <>
      <Header />
      <Content>
        <Outlet />
      </Content>
    </>
  );
};
