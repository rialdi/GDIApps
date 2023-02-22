import { createRouter, createWebHistory } from "vue-router";

import { attrs, loading } from "../auth"
// import { NavigationGuardNext, RouteLocationNormalized, Router } from "vue-router"

import NProgress from "nprogress/nprogress.js";

// Main layout variations
import LayoutSimple from "@/layouts/variations/Simple.vue";
import LayoutLanding from "@/layouts/variations/Landing.vue";
import HomeLanding from "@/layouts/variations/HomeLanding.vue";
import LayoutBackend from "@/layouts/variations/Backend.vue";
import LayoutBackendBoxed from "@/layouts/variations/BackendBoxed.vue";
import LayoutBackendMegaMenu from "@/layouts/variations/BackendMegaMenu.vue";
import LayoutBackendSidebarMiniNav from "@/layouts/variations/BackendSidebarMiniNav.vue";

const Home = () => import("@/views/HomeView.vue");

const Forbidden = () => import("@/pages/errors/Forbidden.vue");

// Backend: Dashboard
const BackendDashboard = () => import("@/pages/backend/DashboardView.vue");

// Auth
const SignIn = () => import("@/pages/Signin.vue");
const SignUp = () => import("@/pages/Signup.vue");
const ForgotPassword = () => import("@/pages/ForgotPassword.vue");
const AuthLock = () => import("@/views/auth/LockView.vue");
const AuthReminder = () => import("@/views/auth/ReminderView.vue");

// Admin: Pages
const UserListPages = () => import("@/pages/user/UserList.vue");
const LookupPages = () => import("@/pages/backend/admins/Lookups.vue");
const ClientPages = () => import("@/pages/backend/admins/Clients.vue");
const ProjectPages = () => import("@/pages/backend/admins/Projects/Index.vue");
const CAddressPages = () => import("@/pages/backend/admins/CAddress/Index.vue");

// Backend: Pages
const BackendPagesAuth = () => import("@/views/backend/pages/AuthView.vue");
const BackendPagesErrors = () => import("@/views/backend/pages/ErrorsView.vue");

// User
const UserProfile = () => import("@/pages/user/Profile.vue");
const UserProfileEdit = () => import("@/pages/user/ProfileEdit.vue");

// Backend: Generic Pages
const BackendPagesGenericBlank = () =>
  import("@/views/backend/pages/generic/BlankView.vue");
const BackendPagesGenericBlankBlock = () =>
  import("@/views/backend/pages/generic/BlankBlockView.vue");
const BackendPagesGenericSearch = () =>
  import("@/views/backend/pages/generic/SearchView.vue");
const BackendPagesGenericProfile = () =>
  import("@/views/backend/pages/generic/ProfileView.vue");
const BackendPagesGenericProfileEdit = () =>
  import("@/views/backend/pages/generic/ProfileEditView.vue");
const BackendPagesGenericInvoice = () =>
  import("@/views/backend/pages/generic/InvoiceView.vue");
const BackendPagesGenericPricingPlans = () =>
  import("@/views/backend/pages/generic/PricingPlansView.vue");
const BackendPagesGenericFaq = () =>
  import("@/views/backend/pages/generic/FaqView.vue");
const BackendPagesGenericTeam = () =>
  import("@/views/backend/pages/generic/TeamView.vue");
const BackendPagesGenericContact = () =>
  import("@/views/backend/pages/generic/ContactView.vue");
const BackendPagesGenericSupport = () =>
  import("@/views/backend/pages/generic/SupportView.vue");
const BackendPagesGenericInbox = () =>
  import("@/views/backend/pages/generic/InboxView.vue");
const BackendPagesGenericSidebarMiniNav = () =>
  import("@/views/backend/pages/generic/SidebarMiniNavView.vue");


const routes = [
  {
    path: "/",
    component: HomeLanding,
    children: [
      {
        path: "",
        name: "home",
        component: Home,
      },
    ],
  },
  {
    path: "/forbidden",
    component: HomeLanding,
    children: [
      {
        path: "",
        name: "forbidden",
        component: Forbidden,
      },
    ],
  },
  {
    path: "/auth",
    component: LayoutSimple,
    children: [
      {
        path: "signin",
        name: "auth-signin",
        component: SignIn,
      },
      {
        path: "signup",
        name: "auth-signup",
        component: SignUp,
      },
      {
        path: "forgotpassword",
        name: "auth-forgotpassword",
        component: ForgotPassword,
      },
      {
        path: "lock",
        name: "auth-lock",
        component: AuthLock,
      },
      {
        path: "reminder",
        name: "auth-reminder",
        component: AuthReminder,
      },
    ],
  },
  {
    path: "/backend",
    redirect: "/backend/dashboard",
    attr:'auth',
    component: LayoutBackend,
    children: [
      {
        path: "dashboard",
        name: "backend-dashboard",
        component: BackendDashboard,
      },
      { 
        path: "admins", 
        redirect: "/admins/lookup",
        children: [
          {
            path: "userlist",
            name: "backend-admins-userlist",
            component: UserListPages,
          },
          {
            path: "lookup",
            name: "backend-admins-lookup",
            component: LookupPages,
          },
          {
            path: "client",
            name: "backend-admins-client",
            component: ClientPages,
          },
          {
            path: "project",
            name: "backend-admins-project",
            component: ProjectPages,
          },
          {
            path: "caddress",
            name: "backend-admins-caddress",
            component: CAddressPages,
          }
        ],
      },
      // {
      //   path: "admins",
      //   redirect: "backend/admins/lookup",
      //   children: [
      //     {
      //       path: "lookup",
      //       name: "admins-lookup",
      //       component: Lookup,
      //     },
      //     {
      //       path: "client",
      //       name: "admins-client",
      //       component: Client,
      //     },
      //     {
      //       path: "project",
      //       name: "admins-project",
      //       component: Project,
      //     },
      //   ]
      // },
      {
        path: "user",
        redirect: "backend/user/profile",
        children: [
          {
            path: "profile",
            name: "user-profile",
            component: UserProfile,
          },
          {
            path: "profile/edit",
            name: "profile-edit",
            component: UserProfileEdit,
          },
        ]
      },
      {
        path: "pages/generic",
        redirect: "/pages/generic/blank",
        children: [
          {
            path: "blank",
            name: "backend-pages-generic-blank",
            component: BackendPagesGenericBlank,
          },
          {
            path: "blank-block",
            name: "backend-pages-generic-blank-block",
            component: BackendPagesGenericBlankBlock,
          },
          {
            path: "search",
            name: "backend-pages-generic-search",
            component: BackendPagesGenericSearch,
          },
          {
            path: "profile",
            name: "backend-pages-generic-profile",
            component: BackendPagesGenericProfile,
          },
          {
            path: "profile-edit",
            name: "backend-pages-generic-profile-edit",
            component: BackendPagesGenericProfileEdit,
          },
          {
            path: "invoice",
            name: "backend-pages-generic-invoice",
            component: BackendPagesGenericInvoice,
          },
          {
            path: "pricing-plans",
            name: "backend-pages-generic-pricing-plans",
            component: BackendPagesGenericPricingPlans,
          },
          {
            path: "faq",
            name: "backend-pages-generic-faq",
            component: BackendPagesGenericFaq,
          },
          {
            path: "team",
            name: "backend-pages-generic-team",
            component: BackendPagesGenericTeam,
          },
          {
            path: "contact",
            name: "backend-pages-generic-contact",
            component: BackendPagesGenericContact,
          },
          {
            path: "support",
            name: "backend-pages-generic-support",
            component: BackendPagesGenericSupport,
          },
          {
            path: "inbox",
            name: "backend-pages-generic-inbox",
            component: BackendPagesGenericInbox,
          },
        ],
      },
    ]
  }
];
    // Create Router
const router = createRouter({
  // mode: 'history',
  history: createWebHistory(),
  linkActiveClass: "active",
  linkExactActiveClass: "",
  scrollBehavior() {
    return { left: 0, top: 0 };
  },
  routes,
});

const invalidAttrRedirect = (to, guardAttr, userAttrs) => userAttrs.indexOf('auth') === -1
        ? '/auth/signin'
        : '/forbidden';

// type RouteGuard = { path, attr }
const ROUTE_GUARDS = [
  { path:'/backend/dashboard', attr:'auth' },
  { path:'/backend/admins', attr:'role:Admin' },
  { path:'/bookings-crud', attr:'role:Employee' },
]
const validateRoute = (to, next, attrs) => {
  // console.log(to);
  for (let i=0; i<ROUTE_GUARDS.length; i++) {
      const { path, attr } = ROUTE_GUARDS[i];
      if (!to.path.startsWith(path)) continue;
      if (attrs.indexOf(attr) === -1) {
          const isAdmin = attrs.indexOf('role:Admin') >= 0;
          const allowAdmin = isAdmin && (attr.startsWith('role:') || attr.startsWith('perm:'));
          if (!allowAdmin) {
              const goTo = invalidAttrRedirect(to, attr, attrs);
              next(goTo);
              return;
          }
      }
  }
  next();
}
  
// NProgress
/*eslint-disable no-unused-vars*/
NProgress.configure({ showSpinner: false });

router.beforeResolve((to, from, next) => {
  
  if (to.name) {
    NProgress.start();
  }
  // console.log(attrs.value);
  validateRoute(to, next, attrs.value);
});

router.afterEach(() => {
  NProgress.done();
});
/*eslint-enable no-unused-vars*/

export default router;