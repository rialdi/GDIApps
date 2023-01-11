<script setup></script>

<template>
  <!-- Hero Content -->
  <div class="bg-primary-dark">
    <div class="content content-full text-center pt-7 pb-5">
      <h1 class="h2 text-white mb-2">Support Center.</h1>
      <h2 class="h4 fw-normal text-white-75">
        Building products is not our only job. We care about our clients.
      </h2>
    </div>
  </div>
  <!-- END Hero Content -->

  <!-- Categories-->
  <div class="content content-boxed">
    <div class="row">
      <div class="col-sm-6 col-md-3">
        <BaseBlock tag="a" href="javascript:void(0)" bordered link-shadow>
          <template #content>
            <div
              class="block-content block-content-full border-bottom text-center"
            >
              <div class="py-3">
                <i class="si si-user fa-2x text-amethyst"></i>
              </div>
            </div>
            <div
              class="block-content block-content-full d-flex align-items-center justify-content-between"
            >
              <span class="fw-semibold text-uppercase fs-sm">Account</span>
              <span class="badge bg-black-50">23</span>
            </div>
          </template>
        </BaseBlock>
      </div>
      <div class="col-sm-6 col-md-3">
        <BaseBlock tag="a" href="javascript:void(0)" bordered link-shadow>
          <template #content>
            <div
              class="block-content block-content-full border-bottom text-center"
            >
              <div class="py-3">
                <i class="si si-settings fa-2x text-city"></i>
              </div>
            </div>
            <div
              class="block-content block-content-full d-flex align-items-center justify-content-between"
            >
              <span class="fw-semibold text-uppercase fs-sm">Features</span>
              <span class="badge bg-black-50">11</span>
            </div>
          </template>
        </BaseBlock>
      </div>
      <div class="col-sm-6 col-md-3">
        <BaseBlock tag="a" href="javascript:void(0)" bordered link-shadow>
          <template #content>
            <div
              class="block-content block-content-full border-bottom text-center"
            >
              <div class="py-3">
                <i class="si si-target fa-2x text-flat"></i>
              </div>
            </div>
            <div
              class="block-content block-content-full d-flex align-items-center justify-content-between"
            >
              <span class="fw-semibold text-uppercase fs-sm">Services</span>
              <span class="badge bg-black-50">16</span>
            </div>
          </template>
        </BaseBlock>
      </div>
      <div class="col-sm-6 col-md-3">
        <BaseBlock tag="a" href="javascript:void(0)" bordered link-shadow>
          <template #content>
            <div
              class="block-content block-content-full border-bottom text-center"
            >
              <div class="py-3">
                <i class="si si-wallet fa-2x text-smooth"></i>
              </div>
            </div>
            <div
              class="block-content block-content-full d-flex align-items-center justify-content-between"
            >
              <span class="fw-semibold text-uppercase fs-sm">Payments</span>
              <span class="badge bg-black-50">19</span>
            </div>
          </template>
        </BaseBlock>
      </div>
    </div>
  </div>
  <!-- END Categories -->

  <!-- Search Form -->
  <div class="bg-body-extra-light">
    <div class="content content-full">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <form @submit.prevent>
            <div class="input-group input-group-lg">
              <input type="text" class="form-control" placeholder="Search.." />
              <span class="input-group-text">
                <i class="fa fa-fw fa-search"></i>
              </span>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
  <!-- END Search Form -->

  <!-- Frequently Asked Questions -->
  <div class="content content-full">
    <div class="row justify-content-center">
      <div class="col-md-8 py-5">
        <!-- Introduction -->
        <h2 class="h3">Introduction</h2>
        <div id="faq1" class="mb-5" role="tablist" aria-multiselectable="true">
          <BaseBlock bordered class="overflow-hidden mb-1">
            <template #content>
              <div
                class="block-header block-header-default"
                role="tab"
                id="faq1_h1"
              >
                <a
                  class="text-muted"
                  data-bs-toggle="collapse"
                  data-bs-parent="#faq1"
                  href="#faq1_q1"
                  aria-expanded="true"
                  aria-controls="faq1_q1"
                  >Welcome to our service!</a
                >
              </div>
              <div
                id="faq1_q1"
                class="collapse show"
                role="tabpanel"
                aria-labelledby="faq1_h1"
                data-bs-parent="#faq1"
              >
                <div class="block-content">
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                </div>
              </div>
            </template>
          </BaseBlock>
          <BaseBlock bordered class="overflow-hidden mb-1">
            <template #content>
              <div
                class="block-header block-header-default"
                role="tab"
                id="faq1_h2"
              >
                <a
                  class="text-muted"
                  data-bs-toggle="collapse"
                  data-bs-parent="#faq1"
                  href="#faq1_q2"
                  aria-expanded="true"
                  aria-controls="faq1_q2"
                  >Who are we?</a
                >
              </div>
              <div
                id="faq1_q2"
                class="collapse"
                role="tabpanel"
                aria-labelledby="faq1_h2"
                data-bs-parent="#faq1"
              >
                <div class="block-content">
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                </div>
              </div>
            </template>
          </BaseBlock>
          <BaseBlock bordered class="overflow-hidden mb-1">
            <template #content>
              <div
                class="block-header block-header-default"
                role="tab"
                id="faq1_h3"
              >
                <a
                  class="text-muted"
                  data-bs-toggle="collapse"
                  data-bs-parent="#faq1"
                  href="#faq1_q3"
                  aria-expanded="true"
                  aria-controls="faq1_q3"
                  >What are our values?</a
                >
              </div>
              <div
                id="faq1_q3"
                class="collapse"
                role="tabpanel"
                aria-labelledby="faq1_h3"
                data-bs-parent="#faq1"
              >
                <div class="block-content">
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                </div>
              </div>
            </template>
          </BaseBlock>
        </div>
        <!-- END Introduction -->

        <!-- Functionality -->
        <h2 class="h3">Functionality</h2>
        <div id="faq2" class="mb-5" role="tablist" aria-multiselectable="true">
          <BaseBlock bordered class="overflow-hidden mb-1">
            <template #content>
              <div
                class="block-header block-header-default"
                role="tab"
                id="faq2_h1"
              >
                <a
                  class="text-muted"
                  data-bs-toggle="collapse"
                  data-bs-parent="#faq2"
                  href="#faq2_q1"
                  aria-expanded="true"
                  aria-controls="faq2_q1"
                  >What are the key features?</a
                >
              </div>
              <div
                id="faq2_q1"
                class="collapse"
                role="tabpanel"
                aria-labelledby="faq2_h1"
                data-bs-parent="#faq2"
              >
                <div class="block-content">
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                </div>
              </div>
            </template>
          </BaseBlock>
          <BaseBlock bordered class="overflow-hidden mb-1">
            <template #content>
              <div
                class="block-header block-header-default"
                role="tab"
                id="faq2_h2"
              >
                <a
                  class="text-muted"
                  data-bs-toggle="collapse"
                  data-bs-parent="#faq2"
                  href="#faq2_q2"
                  aria-expanded="true"
                  aria-controls="faq2_q2"
                  >Does your App support mobile devices?</a
                >
              </div>
              <div
                id="faq2_q2"
                class="collapse"
                role="tabpanel"
                aria-labelledby="faq2_h2"
                data-bs-parent="#faq2"
              >
                <div class="block-content">
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                </div>
              </div>
            </template>
          </BaseBlock>
          <BaseBlock bordered class="overflow-hidden mb-1">
            <template #content>
              <div
                class="block-header block-header-default"
                role="tab"
                id="faq2_h3"
              >
                <a
                  class="text-muted"
                  data-bs-toggle="collapse"
                  data-bs-parent="#faq2"
                  href="#faq2_q3"
                  aria-expanded="true"
                  aria-controls="faq2_q3"
                  >Why should I choose your service?</a
                >
              </div>
              <div
                id="faq2_q3"
                class="collapse"
                role="tabpanel"
                aria-labelledby="faq2_h3"
                data-bs-parent="#faq2"
              >
                <div class="block-content">
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                </div>
              </div>
            </template>
          </BaseBlock>
          <BaseBlock bordered class="overflow-hidden mb-1">
            <template #content>
              <div
                class="block-header block-header-default"
                role="tab"
                id="faq2_h4"
              >
                <a
                  class="text-muted"
                  data-bs-toggle="collapse"
                  data-bs-parent="#faq2"
                  href="#faq2_q4"
                  aria-expanded="true"
                  aria-controls="faq2_q4"
                  >Is my data secure?</a
                >
              </div>
              <div
                id="faq2_q4"
                class="collapse"
                role="tabpanel"
                aria-labelledby="faq2_h4"
                data-bs-parent="#faq2"
              >
                <div class="block-content">
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                </div>
              </div>
            </template>
          </BaseBlock>
        </div>
        <!-- END Functionality -->

        <!-- Payments -->
        <h2 class="h3">Payments</h2>
        <div id="faq3" role="tablist" aria-multiselectable="true">
          <BaseBlock bordered class="overflow-hidden mb-1">
            <template #content>
              <div
                class="block-header block-header-default"
                role="tab"
                id="faq3_h1"
              >
                <a
                  class="text-muted"
                  data-bs-toggle="collapse"
                  data-bs-parent="#faq3"
                  href="#faq3_q1"
                  aria-expanded="true"
                  aria-controls="faq3_q1"
                  >Is there any free plan?</a
                >
              </div>
              <div
                id="faq3_q1"
                class="collapse"
                role="tabpanel"
                aria-labelledby="faq3_h1"
                data-bs-parent="#faq3"
              >
                <div class="block-content">
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                </div>
              </div>
            </template>
          </BaseBlock>
          <BaseBlock bordered class="overflow-hidden mb-1">
            <template #content>
              <div
                class="block-header block-header-default"
                role="tab"
                id="faq3_h2"
              >
                <a
                  class="text-muted"
                  data-bs-toggle="collapse"
                  data-bs-parent="#faq3"
                  href="#faq3_q2"
                  aria-expanded="true"
                  aria-controls="faq3_q2"
                  >What are the available payment options?</a
                >
              </div>
              <div
                id="faq3_q2"
                class="collapse"
                role="tabpanel"
                aria-labelledby="faq3_h2"
                data-bs-parent="#faq3"
              >
                <div class="block-content">
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                </div>
              </div>
            </template>
          </BaseBlock>
          <BaseBlock bordered class="overflow-hidden mb-1">
            <template #content>
              <div
                class="block-header block-header-default"
                role="tab"
                id="faq3_h3"
              >
                <a
                  class="text-muted"
                  data-bs-toggle="collapse"
                  data-bs-parent="#faq3"
                  href="#faq3_q3"
                  aria-expanded="true"
                  aria-controls="faq3_q3"
                  >Can I get a refund?</a
                >
              </div>
              <div
                id="faq3_q3"
                class="collapse"
                role="tabpanel"
                aria-labelledby="faq3_h3"
                data-bs-parent="#faq3"
              >
                <div class="block-content">
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                  <p>
                    Dolor posuere proin blandit accumsan senectus netus nullam
                    curae, ornare laoreet adipiscing luctus mauris adipiscing
                    pretium eget fermentum, tristique lobortis est ut metus
                    lobortis tortor tincidunt himenaeos habitant quis dictumst
                    proin odio sagittis purus mi, nec taciti vestibulum quis in
                    sit varius lorem sit metus mi.
                  </p>
                </div>
              </div>
            </template>
          </BaseBlock>
        </div>
        <!-- END Payments -->
      </div>
    </div>
  </div>
  <!-- END Frequently Asked Questions -->

  <!-- Contact Us -->
  <div class="bg-body-dark">
    <div class="content content-full">
      <div class="my-5 text-center">
        <h3 class="h4 fw-bold mb-4">Can't find what you were looking for?</h3>
        <RouterLink
          :to="{ name: 'backend-pages-generic-contact' }"
          class="btn rounded-pill btn-primary px-4 py-2"
          >Contact Us</RouterLink
        >
      </div>
    </div>
  </div>
  <!-- END Contact Us -->
</template>
