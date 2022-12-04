import { PropsWithChildren } from "react";
import { ContentRoot } from "./Content.styles";

export const Content = ({ children }: PropsWithChildren) => {
  return <ContentRoot>{children}</ContentRoot>;
};
