import { PropsWithChildren } from "react";
import { ContentRoot, ContentWrapper } from "./Content.styles";

export const Content = ({ children }: PropsWithChildren) => {
  return (
    <ContentRoot>
      <ContentWrapper>{children}</ContentWrapper>
    </ContentRoot>
  );
};
