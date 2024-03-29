import { Link, useLocation } from "react-router-dom";
import MainNavItem from "./MainNavItem";
import UserNavItem from "./UserNavItem";
import LogoWhiteNoCircle from "../../../../../assets/logos/svgs/logo-white-no-circle.svg";
import { useContext } from "react";
import AuthContext from "../../contexts/AuthProvider";
import AuthNavItem from "./AuthNavItem";

const MainNavigation = () => {
  const { auth } = useContext(AuthContext);
  const location = useLocation();

  return (
    <nav className="bg-semi-black py-3 px-24">
      <div className="flex flex-wrap items-center justify-between mx-auto">
        <ul className="flex flex-col p-4 md:p-0 mt-4 md:flex-row md:space-x-4 md:mt-0 md:border-0 items-center">
          <Link to="/" className="flex items-center">
            <img
              src={LogoWhiteNoCircle}
              className="h-10 mr-3 inline-block"
              alt="SpartanFitness Logo"
            />
          </Link>
          <MainNavItem path="/">Home</MainNavItem>
          <div className="bg-gray self-stretch w-[1px]" />
          <MainNavItem path="/exercises">Exercises</MainNavItem>
          <MainNavItem path="/coaches/all/workouts" relatedPath="/workouts">
            Workouts
          </MainNavItem>
          <div className="bg-gray self-stretch w-[1px]" />
          <MainNavItem path="/muscles">Muscles</MainNavItem>
          <MainNavItem path="/muscle-groups">Muscle groups</MainNavItem>
        </ul>
        <div className="w-full md:block md:w-auto">
          <ul className="flex flex-col p-4 md:p-0 mt-4 md:flex-row md:space-x-4 md:mt-0 md:border-0 items-center">
            {auth.user && <UserNavItem />}
            {!auth.user && (
              <AuthNavItem
                path={`/login?return_to=${encodeURIComponent(
                  location.pathname,
                )}`}
              >
                Sign in
              </AuthNavItem>
            )}
            {!auth.user && (
              <AuthNavItem path="/signup" border={true}>
                Sign up
              </AuthNavItem>
            )}
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default MainNavigation;
