<script setup>
import { useTemplateStore } from "@/stores/template";
import { ScrollView as kScrollView } from "@progress/kendo-vue-scrollview";
import {
  Card as kCard,
  CardHeader as kCardHeader,
  CardBody as kCardBody,
  CardTitle as kCardTitle,
  CardSubtitle as kCardSubtitle,
  CardImage as kCardImage,
  CardActions as kCardActions,
  Avatar as kAvatar
} from "@progress/kendo-vue-layout";

// Main store
const store = useTemplateStore();

const dsScrollViewImages = [
  {
    title: 'Balloons',
    position: 1,
    url: '/assets/img/home/1.jpeg',
    path: '/home',
    buttons:[
      { text:'Home', class:'btn btn-primary', style:'position: absolute;left: 100px;', path:'/home'},
      { text:'Dashboard', class:'btn btn-danger', style:'position: absolute;left: 300px;', path:'/backend'}
    ]
  },
  {
    title: 'Forest',
    position: 2,
    url: '/assets/img/home/2.jpeg',
  },
  {
    title: 'Flower',
    position: 3,
    url: '/assets/img/home/3.jpeg',
  },
  {
    title: 'Painting',
    position: 4,
    url: '/assets/img/home/4.jpeg',
  },
  {
    title: 'Grass',
    position: 5,
    url: '/assets/img/home/5.jpeg',
  },
]

const dsManagement= [
  { 
    profileImage : "https://labartisan.net/demo/hafsa/assets/images/team/01.jpg",
    profileName : "Sandy Kwary",
    profileTitle : "CEO", 
  },
  { 
    profileImage : "https://labartisan.net/demo/hafsa/assets/images/team/02.jpg",
    profileName : "Aminullah",
    profileTitle : "CTO", 
  },
  { 
    profileImage : "https://labartisan.net/demo/hafsa/assets/images/team/02.jpg",
    profileName : "Hanky",
    profileTitle : "CTO", 
  },
  { 
    profileImage : "https://labartisan.net/demo/hafsa/assets/images/team/02.jpg",
    profileName : "Rialdi",
    profileTitle : "CTO", 
  }
]
</script>

<template>
  <div>
    <!-- Hero -->
    <div id="one-vue-hero" class="bg-body-extra-light">
      <div>
        <kScrollView
          :style="{ width: '100%', height: '384px',}"
          :data-items="dsScrollViewImages"
          :content="'content'"
          :automatic-view-change="true"
          :endless="true"
          :active-view="3"
        >
          <template v-slot:content="{ props }">
            <div class="image-with-text">
              <div v-for="(button, index) in props.item.buttons" :key="index">
                <a :class="button.class" :style="button.style" :href="button.path">{{ button.text }}</a>
              </div>
              <img
                :src="props.item.url"
                :alt="`Photo ${props.item.position}`"
                :style="{ width: '100%', height: '384px',}"
                :draggable="false"
              />
                
            </div>
          </template>
        </kScrollView>
      </div>
      <!-- Management -->
    <div style="text-align:center;">
      <h1>Management</h1>
      <div style="display:flex; justify-content: space-evenly; flex-wrap: wrap;">
        <div v-for="(card, index) in dsManagement" :key="index">
            <card style="width: 260px; box-shadow: 0px 0px 4px 0px rgba(0, 0, 0, .1); margin-top:15px">
                <cardHeader class="k-hbox" style="background: transparent">
                        <avatar type="image" size="medium" shape="circle">
                            <img style="width: 45px; height: 45px;" :src="card.profileImage"/>
                        </avatar>
                        <div>
                        <cardTitle style="margin-bottom: 4px">
                            {{card.profileName}}
                        </cardTitle>
                        <cardSubtitle>
                            <p>{{card.profileTitle}}</p>
                        </cardSubtitle>
                        </div>
                </cardHeader>
                <cardImage :src="card.profileImage" style="height: 185px; max-width: 100%;"/>
            </card>
        </div>
    </div>
    </div>
      <div class="content content-full">
        <div class="row g-0 justify-content-center text-center">
          <div class="col-md-10 pt-7 pb-9">
            <div
              class="d-inline-flex align-items-center space-x-1 fs-sm badge bg-body-dark text-dark mb-2 p-2"
            >
              <i class="fab fa-fw fa-laravel text-danger"></i>
              <span>GDIApps Home 9  version is here!</span>
            </div>
            <h1 class="fs-2 fw-bold mb-3">
              GDI Apps for GDI Employees
            </h1>
            <p class="fs-5 fw-medium text-muted mb-5 mx-xl-8">
              One super flexible UI framework for amazing developers and web
              agencies. Now based on
              <span class="text-body-color fw-semibold">Vue.js 3</span>, with
              <span class="text-body-color fw-semibold">OneUI 5 design</span>
              and <span class="text-body-color fw-semibold">Bootstrap 5</span>
              in core. Comes packed with brand new libraries and tooling,
              including
              <span class="text-body-color fw-semibold">Vite</span>,
              <span class="text-body-color fw-semibold">Pinia</span> and
              <span class="text-body-color fw-semibold">Composition API</span>.
            </p>
            <!-- <RouterLink
              :to="{ name: 'backend-dashboard' }"
              class="btn btn-primary py-2 px-3 m-1"
              v-click-ripple
            >
              <i class="fa fa-fw fa-desktop opacity-50 me-1"></i> Live preview
            </RouterLink> -->
            <RouterLink
              :to="{ name: 'backend-dashboard' }"
              class="btn btn-primary py-2 px-3 m-1"
              v-click-ripple
            >
              <i class="fa fa-fw fa-desktop opacity-50 me-1"></i> Dashboard
            </RouterLink>
            <a
              class="btn btn-alt-primary py-2 px-3 m-1"
              href="https://1.envato.market/AVD6j"
              v-click-ripple
            >
              <i class="fa fa-fw fa-link opacity-50"></i>
              <span class="ms-2">OneUI Package</span>
            </a>
          </div>
        </div>
      </div>
    </div>
    <!-- END Hero -->

    <!-- Hero After -->
    <div id="one-vue-hero-after" class="bg-body-light">
      <div class="content content-full">
        <div class="px-lg-8 text-center">
          <BaseBlock
            fx-shadow
            class="overflow-hidden"
            content-class="p-2"
            style="margin-top: -200px"
          >
            <img
              class="img-fluid"
              src="/assets/media/various/hero-promo.png"
              alt="Hero Promo Light Dashboard"
            />
          </BaseBlock>
        </div>
        <div class="row py-5">
          <div class="col-6 col-md-4">
            <div class="item item-rounded my-4 text-flat bg-flat-lighter">
              <i class="fab fa-fw fa-2x fa-vuejs"></i>
            </div>
            <h4 class="mb-2">Custom Development</h4>
            <p class="text-muted">
              GDI software development services cover the complete project life cycle, 
              from requirement assessment to solution development, 
              to implementation and integration, 
              followed by support and maintenance.
            </p>
          </div>
          <div class="col-6 col-md-4">
            <div class="item item-rounded my-4 text-danger bg-danger-light">
              <i class="fab fa-fw fa-2x fa-laravel"></i>
            </div>
            <h4 class="mb-2">Mobile development</h4>
            <p class="text-muted">
              GDI Mobile Application Development team ensures the applications that enable the users to enjoy extended functionalities of the Wireless Handhelds. GDI talent and creativity will make your Mobile phones more interactive and more stylish
            </p>
          </div>
          <div class="col-6 col-md-4">
            <div class="item item-rounded my-4 text-default bg-default-lighter">
              <i class="fa fa-fw fa-2x fa-circle-notch"></i>
            </div>
            <h4 class="mb-2">Partner</h4>
            <p class="text-muted">
              Document control software from InEight assures project teams have the right project documents to make the right decisions at the right time
            </p>
          </div>
        </div>
      </div>
    </div>
    <!-- END Hero After -->

    <!-- Versions -->
    <div id="one-vue-versions" class="bg-body-extra-light">
      <div class="content content-full">
        <div class="py-5">
          <div class="row mb-5">
            <div class="col-md-6">
              <h2 class="h1 fw-bold mb-2">
                Comes in 2 powerful
                <span class="fw-normal">Versions</span>
              </h2>
              <p class="fs-lg fw-medium text-muted mb-0">
                Either you are building a pure API powered Vue.js app or a
                Laravel Vue.js based one, we've got you covered.
              </p>
            </div>
            <div
              class="col-md-6 d-none d-md-flex align-items-md-center justify-content-md-end"
            >
              <p class="h1 fw-bold text-body-bg-dark mb-0">Fully Loaded.</p>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-6">
            <BaseBlock transparent class="bg-flat-lighter">
              <div class="py-4 py-md-7 text-center">
                <i class="fab fa-fw fa-5x fa-vuejs text-flat"></i>
              </div>
            </BaseBlock>
          </div>
          <div class="col-6">
            <BaseBlock transparent class="bg-danger-light">
              <div class="py-4 py-md-7 text-center">
                <i class="fab fa-fw fa-5x fa-laravel text-city"></i>
              </div>
            </BaseBlock>
          </div>
        </div>
      </div>
    </div>
    <!-- END Versions -->

    <!-- Footer -->
    <footer id="page-footer" class="bg-body-light">
      <div class="content py-5">
        <div class="row fs-sm fw-medium">
          <div class="col-sm-6 order-sm-2 py-1 text-center text-sm-end">
            Created by
            <!-- <i class="fa fa-heart text-danger"></i> by -->
            <a class="fw-semibold" href="http://ptgdi.com"
              >ptgdi</a
            >
          </div>
          <div class="col-sm-6 order-sm-1 py-1 text-center text-sm-start">
            <a class="fw-semibold" href="https://gdiapps.ptgdi.com">{{
              store.app.name + " " + store.app.version
            }}</a> 
            &copy; {{ store.app.copyright }}
          </div>
        </div>
      </div>
    </footer>
    <!-- END Footer -->
  </div>
</template>
